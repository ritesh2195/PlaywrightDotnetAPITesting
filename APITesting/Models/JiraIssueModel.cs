namespace APITesting.Models
{
    public class JiraIssueModel
    {
        public IssueFields? Fields { get; set; }

        public class IssueFields
        {
            public Project? Project { get; set; }
            public string? Summary { get; set; }
            public string? Description { get; set; }
            public IssueType? Issuetype { get; set; }
            public Priority? Priority { get; set; }
        }

        public class Project
        {
            public string? Key { get; set; }
        }

        public class IssueType
        {
            public string? Name { get; set; }
        }

        public class Priority
        {
            public string? Name { get; set; }
        }
    }
}
