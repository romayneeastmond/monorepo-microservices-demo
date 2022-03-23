const Query = {
    getDepartment: async (parent, { id }, { dataSources }, info) => {
        return await dataSources.departmentsService.getDepartment(id);
    },
    getDepartments: async (parent, args, { dataSources }, info) => {
        return await dataSources.departmentsService.getDepartments();
    }
}

const Mutation = {
    addDepartment: async (parent, { department }, { dataSources }, info) => {
        return await dataSources.departmentsService.addDepartment(department);
    },
    deleteDepartment: async (parent, { id }, { dataSources }, info) => {
        return await dataSources.departmentsService.deleteDepartment(id);
    },
    updateDepartment: async (parent, { id, department }, { dataSources }, info) => {
        return await dataSources.departmentsService.updateDepartment(id, department);
    }
}

module.exports = {
    Query, Mutation
};