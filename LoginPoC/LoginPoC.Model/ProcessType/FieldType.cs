namespace LoginPoC.Model.ProcessType
{
    // Available types for process fields
    public enum FieldType
    {
        String,
        TextArea,
        Integer, 
        Decimal,
        Date, // Time? DateTime?
        Bool,
        // Dropdown? Donde se cargan las opciones?

        // Custom Types - Try to autopopulate from user profile
        FirstName,
        LastName,
        BirthDate,
        Gender,
        MaritalStatus,
        Country,
        StateProvince,
        City,
        Address,
        Occupation
    }
}
