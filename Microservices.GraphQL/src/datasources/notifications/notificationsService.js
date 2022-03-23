const { RESTDataSource } = require('apollo-datasource-rest');

class NotificationsService extends RESTDataSource {
    constructor(microserviceUrl) {
        super();

        this.baseURL = microserviceUrl.trim().length === 0
            ? 'http://localhost:5004/'
            : microserviceUrl;
    };

    addNotificationLog = async (log) => {
        const data = await this.post('notification/log/insert', log)
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

    getData = (data) => {
        return {
            id: data.id,
            recipient: data.recipient,
            message: data.message,
            created: data.created
        }
    };

    getNotificationLog = async (id) => {
        const data = await this.get(`notification/log/${encodeURIComponent(id)}`)
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
}

module.exports = NotificationsService;