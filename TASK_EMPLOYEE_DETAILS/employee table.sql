CREATE TABLE employee_curd (
    EmployeeID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    JobTitle NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL, 
   
);
drop table employee_curd
select * from employee_curd