using NetLinqJoinApp;

List<Country> countries = new List<Country>()
{
    new(){ Title = "Russia" },
    new(){ Title = "Usa" },
    new(){ Title = "China" },
};

List<Company> companies = new List<Company>()
{
    new() { Title = "Yandex", Country = countries[0] },
    new() { Title = "Mail Group", Country = countries[0] },
    new() { Title = "Google", Country = countries[1] },
    new() { Title = "TikTok", Country = countries[2] },
    new() { Title = "Ozon", Country = countries[0] },
    new() { Title = "Twitter", Country = countries[1] },
};

List<Employee> employees = new List<Employee>()
{
    new() { Name = "Bob", Age = 24, Company = companies[0] },
    new() { Name = "Joe", Age = 33, Company = companies[1] },
    new() { Name = "Sam", Age = 19, Company = companies[2] },
    new() { Name = "Tim", Age = 28, Company = companies[3] },
    new() { Name = "Tom", Age = 32, Company = companies[4] },
    new() { Name = "Jim", Age = 46, Company = companies[5] },
    new() { Name = "Ben", Age = 29, Company = companies[0] },
    new() { Name = "Leo", Age = 37, Company = companies[1] },
    new() { Name = "Max", Age = 43, Company = companies[3] },
    new() { Name = "Ann", Age = 25, Company = companies[4] },
};

var emplCompanyO = from e in employees
                   join c in companies on e?.Company?.Title equals c.Title
                   select new
                   {
                       EmployeName = e.Name,
                       CompanyTitle = c.Title
                   };
foreach(var e in emplCompanyO)
    Console.WriteLine($"{e.EmployeName} {e.CompanyTitle}");
Console.WriteLine();

var emplCompanyM = employees.Join(companies,
                                e => e.Company.Title,
                                c => c.Title,
                                (e, c) => new
                                {
                                    EmployeName = e.Name,
                                    CompanyTitle = c.Title
                                }).OrderBy(e => e.EmployeName);

foreach (var e in emplCompanyM)
    Console.WriteLine($"{e.EmployeName} {e.CompanyTitle}");
Console.WriteLine();

var eJoinGroupO = from c in companies
                  join e in employees on c.Title equals e.Company.Title into empl
                  select new
                  {
                      Company = c.Title,
                      Employees = empl
                  };

foreach(var c in eJoinGroupO)
{
    Console.WriteLine($"Company: {c.Company}");
    foreach (var e in c.Employees)
        Console.WriteLine($"\t{e.Name} {e.Age}");
}
Console.WriteLine();

var eJoinGroupM = companies.GroupJoin(employees,
                            c => c.Title,
                            e => e.Company.Title,
                            (c, empl) => new
                            {
                                Title = c.Title,
                                Employees = empl
                            });
foreach (var c in eJoinGroupM)
{
    Console.WriteLine($"Company: {c.Title}");
    foreach (var e in c.Employees)
        Console.WriteLine($"\t{e.Name} {e.Age}");
}
Console.WriteLine();
