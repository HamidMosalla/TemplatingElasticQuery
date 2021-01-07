using System.Collections.Generic;

namespace TemplatingPrototype
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> Tags { get; set; }
    }
}