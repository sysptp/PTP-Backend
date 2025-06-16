param(
    [Parameter(Mandatory=$true)]
    [string]$TableDefinition
)

# Funci�n para extraer propiedades de la definici�n de tabla (versi�n de depuraci�n)
function Get-PropertiesFromTableDefinition {
    param (
        [string]$TableDefinition
    )
    
    Write-Host "`n--- Iniciando extracci�n de propiedades ---" -ForegroundColor Yellow
    
    $properties = @()
    
    # Limpiar la definici�n de tabla para ver qu� estamos procesando
    Write-Host "`nDefinici�n de tabla limpia:" -ForegroundColor Yellow
    $cleanedDefinition = $TableDefinition -replace 'CREATE\s+TABLE\s+[^\(]+\(', ''
    $cleanedDefinition = $cleanedDefinition -replace ',\s*CONSTRAINT[^,]+', ''
    $cleanedDefinition = $cleanedDefinition -replace '\);?$', ''
    Write-Host $cleanedDefinition
    
    # Dividir por comas que no est�n dentro de par�ntesis
    $columnDefinitions = $cleanedDefinition -split ',(?![^(]*\))'
    
    Write-Host "`nDefiniciones de columnas encontradas: $($columnDefinitions.Count)" -ForegroundColor Yellow
    
    foreach ($columnDef in $columnDefinitions) {
        $columnDef = $columnDef.Trim()
        
        Write-Host "`nProcesando: '$columnDef'" -ForegroundColor Cyan
        
        # Saltar l�neas vac�as
        if ([string]::IsNullOrWhiteSpace($columnDef)) {
            Write-Host "  L�nea vac�a - saltando" -ForegroundColor Red
            continue
        }
        
        # Saltar definiciones de CONSTRAINT
        if ($columnDef -match '^\s*CONSTRAINT') {
            Write-Host "  Es una constraint - saltando" -ForegroundColor Red
            continue
        }
        
        # Extraer nombre de columna y tipo con una expresi�n regular m�s simple
        if ($columnDef -match '^(\w+)\s+(\w+)') {
            $propertyName = $matches[1]
            $sqlType = $matches[2]
            
            Write-Host "  Nombre: $propertyName, Tipo SQL: $sqlType" -ForegroundColor Green
            
            # Ignorar propiedades de auditor�a
            if ($propertyName -in @('Borrado', 'FechaAdicion', 'UsuarioAdicion', 'FechaModificacion', 'UsuarioModificacion')) {
                Write-Host "  Propiedad de auditor�a - saltando" -ForegroundColor Red
                continue
            }
            
            # Determinar si es nullable
            $isNullable = !($columnDef -match '\sNOT\s+NULL')
            Write-Host "  Nullable: $isNullable" -ForegroundColor Green
            
            # Obtener valor por defecto
            $defaultValue = $null
            if ($columnDef -match 'DEFAULT\s+([^\s,]+)') {
                $defaultValue = $matches[1]
                $defaultValue = $defaultValue -replace '^[''"]|[''"]$', ''  # Remover comillas
                $defaultValue = $defaultValue -replace '\(\)$', ''  # Remover par�ntesis de funciones
                Write-Host "  Valor por defecto: $defaultValue" -ForegroundColor Green
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
            Write-Host "  Tipo C#: $csharpType" -ForegroundColor Green
            
            # Determinar si es clave primaria
            $isPrimaryKey = $columnDef -match 'IDENTITY\(1,1\)' -or $propertyName -eq 'Id'
            Write-Host "  Es clave primaria: $isPrimaryKey" -ForegroundColor Green
            
            # Determinar si es clave for�nea
            $isForeignKey = $TableDefinition -match "FOREIGN\s+KEY\s*\($propertyName\)"
            Write-Host "  Es clave for�nea: $isForeignKey" -ForegroundColor Green
            
            $property = @{
                Name = $propertyName
                Type = $csharpType
                Nullable = $isNullable
                DefaultValue = $defaultValue
                IsForeignKey = $isForeignKey
                IsPrimaryKey = $isPrimaryKey
            }
            
            $properties += $property
            Write-Host "  Propiedad agregada exitosamente" -ForegroundColor Green
        } else {
            Write-Host "  No se pudo extraer nombre y tipo de columna" -ForegroundColor Red
        }
    }
    
    Write-Host "`n--- Total de propiedades extra�das: $($properties.Count) ---" -ForegroundColor Yellow
    
    foreach ($prop in $properties) {
        Write-Host "`nPropiedad: $($prop.Name)"
        Write-Host "  Tipo: $($prop.Type)"
        Write-Host "  Nullable: $($prop.Nullable)"
        Write-Host "  Valor por defecto: $($prop.DefaultValue)"
        Write-Host "  Es clave primaria: $($prop.IsPrimaryKey)"
        Write-Host "  Es clave for�nea: $($prop.IsForeignKey)"
    }
    
    return $properties
}

# Ejecutar la prueba
$properties = Get-PropertiesFromTableDefinition -TableDefinition $TableDefinition

Write-Host "`n--- Resumen final ---" -ForegroundColor Green
Write-Host "Total de propiedades encontradas: $($properties.Count)"