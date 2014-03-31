namespace sql_string_generator
{
    public interface IBuildUpdateSqlStatements<in Model>
    {
        string build(Model model);
    }
}