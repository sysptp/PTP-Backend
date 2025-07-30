param(
    [Parameter(Mandatory=$true)]
    [string]$ModelName,
    
    [Parameter(Mandatory=$true)]
    [string]$TableDefinition
)

# Configuración de rutas base
$SolutionPath = Get-Location
$DataLayerPath = Join-Path $SolutionPath "DataLayer"
$BusinessLayerPath = Join-Path $SolutionPath "BussinessLayer"
$APIPath = Join-Path $SolutionPath "PTP_API"

# Rutas específicas para cada tipo de archivo
$Paths = @{
    Model = Join-Path $DataLayerPath "Models\ModuloCitas"
    DTORequest = Join-Path $BusinessLayerPath "DTOs\ModuloCitas\$ModelName"
    DTOResponse = Join-Path $BusinessLayerPath "DTOs\ModuloCitas\$ModelName"
    RepositoryInterface = Join-Path $BusinessLayerPath "Interfaces\Repository\ModuloCitas"
    Repository = Join-Path $BusinessLayerPath "Repository\ModuloCitas"
    ServiceInterface = Join-Path $BusinessLayerPath "Interfaces\Services\ModuloCitas"
    Service = Join-Path $BusinessLayerPath "Services\ModuloCitas"
    Controller = Join-Path $APIPath "Controllers\ModuloCita"
    Validator = Join-Path $BusinessLayerPath "FluentValidations\ModuloCitas"
}

# Crear directorios si no existen
foreach ($path in $Paths.Values) {
    if (-not (Test-Path $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

# Función para extraer propiedades de la definición de tabla
function Get-PropertiesFromTableDefinition {
    param (
        [string]$TableDefinition
    )
    
    $properties = @()
    
    # Limpiar la definición de tabla
    $cleanedDefinition = $TableDefinition -replace 'CREATE\s+TABLE\s+[^\(]+\(', ''
    $cleanedDefinition = $cleanedDefinition -replace ',\s*CONSTRAINT[^,]+', ''
    $cleanedDefinition = $cleanedDefinition -replace '\);?$', ''
    
    # Dividir por comas que no estén dentro de paréntesis
    $columnDefinitions = $cleanedDefinition -split ',(?![^(]*\))'
    
    foreach ($columnDef in $columnDefinitions) {
        $columnDef = $columnDef.Trim()
        
        # Saltar líneas vacías
        if ([string]::IsNullOrWhiteSpace($columnDef)) {
            continue
        }
        
        # Saltar definiciones de CONSTRAINT
        if ($columnDef -match '^\s*CONSTRAINT') {
            continue
        }
        
        # Extraer nombre de columna y tipo
        if ($columnDef -match '^(\w+)\s+(\w+)') {
            $propertyName = $matches[1]
            $sqlType = $matches[2]
            
            # Ignorar propiedades de auditoría
            if ($propertyName -in @('Borrado', 'FechaAdicion', 'UsuarioAdicion', 'FechaModificacion', 'UsuarioModificacion')) {
                continue
            }
            
            # Determinar si es nullable
            $isNullable = !($columnDef -match '\sNOT\s+NULL')
            
            # Obtener valor por defecto
            $defaultValue = $null
            if ($columnDef -match 'DEFAULT\s+([^\s,]+)') {
                $defaultValue = $matches[1]
                $defaultValue = $defaultValue -replace '^[''"]|[''"]$', ''  # Remover comillas
                $defaultValue = $defaultValue -replace '\(\)$', ''  # Remover paréntesis de funciones
            }
            
            # Mapear tipos SQL a tipos C#
            $csharpType = switch ($sqlType) {
                'bigint' { 'long' }
                'int' { 'int' }
                'nvarchar' { 'string' }
                'varchar' { 'string' }
                'datetime' { 'DateTime' }
                'bit' { 'bool' }
                'decimal' { 'decimal' }
                'float' { 'double' }
                'time' { 'TimeSpan' }
                default { 'string' }
            }
            
            # Determinar si es clave primaria
            $isPrimaryKey = $columnDef -match 'IDENTITY\(1,1\)' -or $propertyName -eq 'Id'
            
            # Determinar si es clave foránea
            $isForeignKey = $TableDefinition -match "FOREIGN\s+KEY\s*\($propertyName\)"
            
            $properties += @{
                Name = $propertyName
                Type = $csharpType
                Nullable = $isNullable
                DefaultValue = $defaultValue
                IsForeignKey = $isForeignKey
                IsPrimaryKey = $isPrimaryKey
            }
        }
    }
    
    return $properties
}# Función para procesar plantillas con propiedades (versión final corregida)
function Process-Template {
    param (
        [string]$TemplatePath,
        [string]$OutputPath,
        [hashtable]$Parameters,
        [array]$Properties
    )
    
    # Leer el contenido de la plantilla
    $templateContent = Get-Content $TemplatePath -Raw
    
    # Reemplazar parámetros básicos
    foreach ($key in $Parameters.Keys) {
        $templateContent = $templateContent -replace "\{\{$key\}\}", $Parameters[$key]
    }
    
    # Procesar el bloque {{#each Properties}}
    if ($templateContent -match '\{\{#each Properties\}\}([\s\S]*?)\{\{/each\}\}') {
        $propertyBlock = $matches[1]
        $processedProperties = @()
        
        foreach ($prop in $Properties) {
            $propContent = $propertyBlock
            
            # Reemplazar valores de propiedad básicos
            $propContent = $propContent -replace '\{\{Name\}\}', $prop.Name
            $propContent = $propContent -replace '\{\{Type\}\}', $prop.Type
            
            # Procesar condicionales if IsPrimaryKey
            if ($prop.IsPrimaryKey) {
                $propContent = $propContent -replace '\{\{#if IsPrimaryKey\}\}([\s\S]*?)\{\{/if\}\}', '$1'
            } else {
                $propContent = $propContent -replace '\{\{#if IsPrimaryKey\}\}[\s\S]*?\{\{/if\}\}', ''
            }
            
            # Procesar condicionales if Nullable
            if ($prop.Nullable) {
                $propContent = $propContent -replace '\{\{#if Nullable\}\}\?\{\{/if\}\}', '?'
                $propContent = $propContent -replace '\{\{#if Nullable\}\}([\s\S]*?)\{\{/if\}\}', '$1'
            } else {
                $propContent = $propContent -replace '\{\{#if Nullable\}\}\?\{\{/if\}\}', ''
                $propContent = $propContent -replace '\{\{#if Nullable\}\}[\s\S]*?\{\{/if\}\}', ''
            }
            
            # Procesar condicionales if IsForeignKey
            if ($prop.IsForeignKey) {
                $propContent = $propContent -replace '\{\{#if IsForeignKey\}\}([\s\S]*?)\{\{/if\}\}', '$1'
            } else {
                $propContent = $propContent -replace '\{\{#if IsForeignKey\}\}[\s\S]*?\{\{/if\}\}', ''
            }
            
            # Procesar condicionales if (Name == "MessageTypeId")
            if ($prop.Name -eq "MessageTypeId") {
                $propContent = $propContent -replace '\{\{#if \(Name == "MessageTypeId"\)\}\}([\s\S]*?)\{\{/if\}\}', '$1'
            } else {
                $propContent = $propContent -replace '\{\{#if \(Name == "MessageTypeId"\)\}\}[\s\S]*?\{\{/if\}\}', ''
            }
            
            # Procesar condicionales complejos (Type == "string" && !Nullable)
            if ($prop.Type -eq "string" -and -not $prop.Nullable) {
                $propContent = $propContent -replace '\{\{#if \(Type == "string" && !Nullable\)\}\}([\s\S]*?)\{\{/if\}\}', '$1'
            } else {
                $propContent = $propContent -replace '\{\{#if \(Type == "string" && !Nullable\)\}\}[\s\S]*?\{\{/if\}\}', ''
            }
            
            # Procesar condicionales (Type == "bool" && !Nullable)
            if ($prop.Type -eq "bool" -and -not $prop.Nullable) {
                $propContent = $propContent -replace '\{\{#if \(Type == "bool" && !Nullable\)\}\}([\s\S]*?)\{\{else if \(Type == "string" && !Nullable\)\}\}[\s\S]*?\{\{/if\}\}', '$1'
            } elseif ($prop.Type -eq "string" -and -not $prop.Nullable) {
                $propContent = $propContent -replace '\{\{#if \(Type == "bool" && !Nullable\)\}\}[\s\S]*?\{\{else if \(Type == "string" && !Nullable\)\}\}([\s\S]*?)\{\{/if\}\}', '$1'
            } else {
                $propContent = $propContent -replace '\{\{#if \(Type == "bool" && !Nullable\)\}\}[\s\S]*?\{\{else if \(Type == "string" && !Nullable\)\}\}[\s\S]*?\{\{/if\}\}', ''
            }
            
            # Procesar condicionales DefaultValue
            if ($prop.DefaultValue) {
                $propContent = $propContent -replace '\{\{#if DefaultValue\}\}([\s\S]*?)\{\{/if\}\}', '$1'
                $propContent = $propContent -replace '\{\{DefaultValue\}\}', $prop.DefaultValue
            } else {
                $propContent = $propContent -replace '\{\{#if DefaultValue\}\}[\s\S]*?\{\{/if\}\}', ''
            }
            
            # Limpieza final: eliminar cualquier resto de {{/if}} que pueda quedar
            $propContent = $propContent -replace '\{\{/if\}\}', ''
            
            if ($propContent.Trim()) {
                $processedProperties += $propContent
            }
        }
        
        # Reemplazar el bloque de propiedades con el contenido procesado
        $propertiesContent = $processedProperties -join ""
        $templateContent = $templateContent -replace '\{\{#each Properties\}\}[\s\S]*?\{\{/each\}\}', $propertiesContent
    }
    
    # Limpieza final: eliminar cualquier {{/if}} que pueda quedar en todo el contenido
    $templateContent = $templateContent -replace '\{\{/if\}\}', ''
    
    # Guardar el resultado
    $templateContent | Set-Content -Path $OutputPath -Force
}

# Extraer propiedades
$properties = Get-PropertiesFromTableDefinition -TableDefinition $TableDefinition

# Parámetros para las plantillas
$templateParams = @{
    ModelName = $ModelName
    ModelNameLower = $ModelName.ToLower()
}

# Ruta de las plantillas
$templatesPath = Join-Path $SolutionPath "Templates"

# Generar Model
Process-Template -TemplatePath (Join-Path $templatesPath "Model.tt") `
                 -OutputPath (Join-Path $Paths.Model "$ModelName.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

# Generar DTOs
Process-Template -TemplatePath (Join-Path $templatesPath "DTORequest.tt") `
                 -OutputPath (Join-Path $Paths.DTORequest "${ModelName}Request.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

Process-Template -TemplatePath (Join-Path $templatesPath "DTOResponse.tt") `
                 -OutputPath (Join-Path $Paths.DTOResponse "${ModelName}Response.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

# Generar Repository
Process-Template -TemplatePath (Join-Path $templatesPath "RepositoryInterface.tt") `
                 -OutputPath (Join-Path $Paths.RepositoryInterface "I${ModelName}Repository.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

Process-Template -TemplatePath (Join-Path $templatesPath "Repository.tt") `
                 -OutputPath (Join-Path $Paths.Repository "${ModelName}Repository.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

# Generar Service
Process-Template -TemplatePath (Join-Path $templatesPath "ServiceInterface.tt") `
                 -OutputPath (Join-Path $Paths.ServiceInterface "I${ModelName}Service.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

Process-Template -TemplatePath (Join-Path $templatesPath "Service.tt") `
                 -OutputPath (Join-Path $Paths.Service "${ModelName}Service.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

# Generar Controller
Process-Template -TemplatePath (Join-Path $templatesPath "Controller.tt") `
                 -OutputPath (Join-Path $Paths.Controller "${ModelName}Controller.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

# Generar Validator
Process-Template -TemplatePath (Join-Path $templatesPath "Validator.tt") `
                 -OutputPath (Join-Path $Paths.Validator "${ModelName}RequestValidator.cs") `
                 -Parameters $templateParams `
                 -Properties $properties

Write-Host "Archivos generados exitosamente para $ModelName"
Write-Host "Por favor, no olvides:"
Write-Host "1. Agregar los mapeos en CitasProfile.cs"
Write-Host "2. Registrar los servicios en el contenedor de DI"
Write-Host "3. Agregar el DbSet<$ModelName> en PDbContext.cs"