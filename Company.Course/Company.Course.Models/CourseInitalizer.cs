namespace Company.Course.Models
{
    public static class CourseInitalizer
    {
        public static void Initialize(CourseDbContext db)
        {
            if (db.Courses.Any())
            {
                return;
            }

            var courses = new List<Course>
            {
                new Course { Id = Guid.NewGuid(), Name = "Microservices 101", Description = "An Introduction to Microservices." },
                new Course { Id = Guid.NewGuid(), Name = "Terraform on Azure", Description = "Creating Azure resources with Terraform." },
                new Course { Id = Guid.NewGuid(), Name = "Deploying .NET Microservices on Kubernetes", Description = "How to deploy microservices in k8s." },
                new Course { Id = Guid.NewGuid(), Name = "Building Microservies with Docker", Description = "Dockerfile creation in Visual Studio Enterprise." },
                new Course { Id = Guid.NewGuid(), Name = "Docker Compose in a Nutshell", Description = "Defining multiple container environments." },
                new Course { Id = Guid.NewGuid(), Name = "Bring the Lettuce with RabbitMQ", Description = "There is actually no lettuce or rabbits." },
                new Course { Id = Guid.NewGuid(), Name = "MassTransit and RabbitMQ", Description = "Enterprise service bus written in .NET for RabbitMQ." }
            };

            db.Courses.AddRange(courses);

            db.SaveChanges();
        }
    }
}
