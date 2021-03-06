const Query = {
    getEmployee: async (parent, { id }, { dataSources }, info) => {
        var employee = await dataSources.employeesService.getEmployee(id);

        employee.department = await EmployeeHelper.getDepartment(dataSources.departmentsService, employee);

        return employee;
    },
    getEmployees: async (parent, args, { dataSources }, info) => {
        var employees = await dataSources.employeesService.getEmployees();

        await EmployeeHelper.getDepartments(dataSources.departmentsService, employees);

        return employees;
    },
    getEmployeesByDepartment: async (parent, { departmentId }, { dataSources }, info) => {
        var employees = await dataSources.employeesService.getEmployeesByDepartment(departmentId);

        await EmployeeHelper.getDepartments(dataSources.departmentsService, employees);

        return employees;
    },
    getEmployeesByStatus: async (parent, { status }, { dataSources }, info) => {
        var employees = await dataSources.employeesService.getEmployeesByStatus(status);

        await EmployeeHelper.getDepartments(dataSources.departmentsService, employees);

        return employees;
    }
}

const Mutation = {
    addEmployee: async (parent, { employee }, { dataSources }, info) => {
        var employee = await dataSources.employeesService.addEmployee(employee);

        employee.department = await EmployeeHelper.getDepartment(dataSources.departmentsService, employee);

        return employee;
    },
    deleteEmployee: async (parent, { id }, { dataSources }, info) => {
        return await dataSources.employeesService.deleteEmployee(id);
    },
    updateEmployee: async (parent, { id, employee }, { dataSources }, info) => {
        var employee = await dataSources.employeesService.updateEmployee(id, employee);

        employee.department = await EmployeeHelper.getDepartment(dataSources.departmentsService, employee);

        return employee;
    }
}

const EmployeeHelper = {
    getDepartment: async (departmentsService, employee) => {
        if (employee !== null && employee.departmentId !== null) {
            return await departmentsService.getDepartment(employee.departmentId);
        }

        return null;
    },
    getDepartments: async (departmentsService, employees) => {
        var departments = await departmentsService.getDepartments();

        employees.forEach((employee) => {
            if (employee !== null && employee.departmentId !== null) {
                employee.department = departments.find(x => x.id === employee.departmentId);
            }
        });
    }
}

module.exports = {
    Query, Mutation
};