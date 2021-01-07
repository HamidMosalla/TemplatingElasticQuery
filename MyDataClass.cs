namespace TemplatingPrototype
{
    //var @class = new MyDataClass { SortField = "Title" };
    //var valval = JsonTemplateVisitor.Serialize(@class, query2);
    public class MyDataClass
    {
        public string FieldName { get; set; }
        public string Terms { get; set; }
        public string SortField { get; set; }
    }
}