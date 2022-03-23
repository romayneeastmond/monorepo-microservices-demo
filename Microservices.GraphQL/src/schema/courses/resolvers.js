const Query = {
    getCourse: async (parent, { id }, { dataSources }, info) => {
        return await dataSources.coursesService.getCourse(id);
    },
    getCourses: async (parent, args, { dataSources }, info) => {
        return await dataSources.coursesService.getCourses();
    }
}

const Mutation = {
    addCourse: async (parent, { course }, { dataSources }, info) => {
        return await dataSources.coursesService.addCourse(course);
    },
    deleteCourse: async (parent, { id }, { dataSources }, info) => {
        return await dataSources.coursesService.deleteCourse(id);
    },
    updateCourse: async (parent, { id, course }, { dataSources }, info) => {
        return await dataSources.coursesService.updateCourse(id, course);
    }
}

module.exports = {
    Query, Mutation
};