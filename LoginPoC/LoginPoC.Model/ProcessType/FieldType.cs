using System.ComponentModel.DataAnnotations;

namespace LoginPoC.Model.ProcessType
{
    // Available types for process fields
    public enum FieldType
    {
        [Display(Name = "Texto corto")]
        String,
        [Display(Name = "Texto largo")]
        TextArea,
        [Display(Name = "Número entero")]
        Integer,
        [Display(Name = "Número decimal")]
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
