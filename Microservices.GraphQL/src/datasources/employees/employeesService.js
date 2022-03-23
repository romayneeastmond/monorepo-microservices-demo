const { RESTDataSource } = require('apollo-datasource-rest');

class EmployeesService extends RESTDataSource {
    constructor(microserviceUrl) {
        super();

        this.baseURL = microserviceUrl.trim().length === 0
            ? 'http://localhost:5003/'
            : microserviceUrl;
    };

    addEmployee = async (employee) => {
        const data = await this.post('employees/insert', employee)
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null || (data !== null && data.acknowledged === false)) {
            return null;
        }

        return this.getData(data);
    };

    deleteEmployee = async (id) => {
        const data = await this.delete(`employees/delete/?id=${encodeURIComponent(id)}`)
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null || (data !== null && data.value === null)) {
            return false;
        }

        return true;
    };

    getData = (data) => {
        return {
            id: data.id,
            departmentId: data.departmentId,
            firstName: data.firstName,
            lastName: data.lastName,
            emailAddress: data.emailAddress,
            isActive: data.isActive
        }
    };

    getEmployee = async (id) => {
        const data = await this.get(`employee/${encodeURIComponent(id)}`)
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null) {
            return null;
        }

        return this.getData(data);
    };

    getEmployees = async () => {
        const data = await this.get('employees')
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null) {
            return null;
        }

        let employees = [];

        data.forEach((employee) => {
            employees.push(this.getData(employee))
        });

        return employees;
    };

    updateEmployee = async (id, employee) => {
        const data = await this.put(`employees/update?id=${encodeURIComponent(id)}`, employee)
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null || (data !== null && data.value === null)) {
            return null;
        }

        return this.getData({ id, ...employee });
    };
}

module.exports = EmployeesService;