using System;
using System.Collections.Generic;

namespace BussinessLayer.Helpers.CargaMasivaHelpers
{
    public class EntityMapper 
    {
        public object MapToEntity(string[] values, List<(string Name, bool IsIdentity, bool IsNullable)> columnDetails, Type entityType)
        {
            var entityInstance = Activator.CreateInstance(entityType);

            for (int i = 0; i < columnDetails.Count; i++)
            {
                var columnDetail = columnDetails[i];
                var property = entityType.GetProperty(columnDetail.Name);

                if (property != null)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(values[i]) && columnDetail.IsNullable)
                        {
                            continue;
                        }

                        object value;

                        if (property.PropertyType == typeof(bool))
                        {
                            value = values[i] == "1" ? true : false;
                        }
                        else
                        {
                            value = Convert.ChangeType(values[i], property.PropertyType);
                        }

                        property.SetValue(entityInstance, value);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"Error en columna '{columnDetail.Name}' con valor '{values[i]}': {ex.Message}");
                    }
                }
            }

            return entityInstance;
        }

    }
}

