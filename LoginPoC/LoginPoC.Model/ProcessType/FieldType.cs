using System.ComponentModel.DataAnnotations;

namespace LoginPoC.Model.ProcessType
{
    // Available types for process fields
    public enum FieldType
    {
        [Display(Name = "Texto corto (250 caracteres)")]
        String,
        [Display(Name = "Texto largo (5000 caracteres)")]
        TextArea,
        [Display(Name = "Número entero")]
        Integer,
        [Display(Name = "Número con decimales")]
        Decimal,
        [Display(Name = "Fecha")]
        Date, // Time? DateTime?
        [Display(Name = "Sí/No")]
        Bool,
        // Dropdown? Donde se cargan las opciones?

        // Custom Types - Try to autopopulate from user profile
        [Display(Name = "Primer Nombre")]
        FirstName,
        [Display(Name = "Apellido")]
        LastName,
        [Display(Name = "Fecha de nacimiento")]
        BirthDate,
        [Display(Name = "Género")]
        Gender,
        [Display(Name = "Estado civil")]
        MaritalStatus,
        [Display(Name = "País")]
        Country,
        [Display(Name = "Estado/Provincia")]
        StateProvince,
        [Display(Name = "Ciudad")]
        City,
        [Display(Name = "Dirección")]
        Address,
        [Display(Name = "Ocupación")]
        Occupation
    }
}
