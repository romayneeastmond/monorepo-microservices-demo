const { RESTDataSource } = require('apollo-datasource-rest');

class DepartmentsService extends RESTDataSource {
    constructor(microserviceUrl) {
        super();

        this.baseURL = microserviceUrl.trim().length === 0
            ? 'http://localhost:5002/'
            : microserviceUrl;
    };

    addDepartment = async (department) => {
        const data = await this.post('departments/insert', department)
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

    deleteDepartment = async (id) => {
        const data = await this.delete(`departments/delete/?id=${encodeURIComponent(id)}`)
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
            name: data.name,
            code: data.code
        }
    };

    getDepartment = async (id) => {
        const data = await this.get(`department/${encodeURIComponent(id)}`)
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

    getDepartments = async () => {
        const data = await this.get('departments')
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null) {
            return null;
        }

        let departments = [];

        data.forEach((department) => {
            departments.push(this.getData(department))
        });

        return departments;
    };

    updateDepartment = async (id, department) => {
        const data = await this.put(`departments/update?id=${encodeURIComponent(id)}`, department)
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null || (data !== null && data.value === null)) {
            return null;
        }

        return this.getData({ id, ...department });
    };
}

module.exports = DepartmentsService;