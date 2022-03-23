const Query = {
    getNotificationLog: async (parent, { id }, { dataSources }, info) => {
        return await dataSources.notificationsService.getNotificationLog(id);
    }
}

const Mutation = {
    addNotificationLog: async (parent, { notificationLog }, { dataSources }, info) => {
        return await dataSources.notificationsService.addNotificationLog(notificationLog);
    }
}

module.exports = {
    Query, Mutation
};