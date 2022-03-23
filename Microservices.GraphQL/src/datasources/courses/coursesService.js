const { RESTDataSource } = require('apollo-datasource-rest');

class CoursesService extends RESTDataSource {
    constructor(microserviceUrl) {
        super();

        this.baseURL = microserviceUrl.trim().length === 0
            ? 'http://localhost:5001/'
            : microserviceUrl;
    };

    addCourse = async (course) => {
        const data = await this.post('courses/insert', course)
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

    deleteCourse = async (id) => {
        const data = await this.delete(`courses/delete/?id=${encodeURIComponent(id)}`)
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
            description: data.description
        }
    };

    getCourse = async (id) => {
        const data = await this.get(`course/${encodeURIComponent(id)}`)
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

    getCourses = async () => {
        const data = await this.get('courses')
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null) {
            return null;
        }

        let courses = [];

        data.forEach((course) => {
            courses.push(this.getData(course))
        });

        return courses;
    };

    updateCourse = async (id, course) => {
        const data = await this.put(`courses/update?id=${encodeURIComponent(id)}`, course)
            .then((result) => {
                return result;
            }).catch((error) => {
                console.log(error);
            });

        if (data == null || (data !== null && data.value === null)) {
            return null;
        }

        return this.getData({ id, ...course });
    };
}

module.exports = CoursesService;