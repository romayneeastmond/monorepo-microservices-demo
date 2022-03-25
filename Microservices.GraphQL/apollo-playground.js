const { ApolloServer, gql } = require('apollo-server-express');
const { ApolloServerPluginLandingPageGraphQLPlayground } = require('apollo-server-core');
const express = require('express');
const http = require('http');
const fs = require('fs');
const merge = require('lodash.merge');

const CoursesService = require('./src/datasources/courses/coursesService');
const DepartmentsService = require('./src/datasources/departments/departmentsService');
const EmployeesService = require('./src/datasources/employees/employeesService');
const NotificationsService = require('./src/datasources/notifications/notificationsService');

require('dotenv').config();

(async () => {
    const app = express();
    const router = express.Router();

    router.get('/', function (req, res) {
        res.redirect('/graphql');
    });

    app.use('/', router);

    app.set('port', process.env.PORT || 4000);

    const httpServer = http.createServer(app);

    const typeDefsCourses = gql(fs.readFileSync('./src/schema/courses/schema.graphql', { encoding: 'utf8' }));
    const typeDefsDepartments = gql(fs.readFileSync('./src/schema/departments/schema.graphql', { encoding: 'utf8' }));
    const typeDefsEmployees = gql(fs.readFileSync('./src/schema/employees/schema.graphql', { encoding: 'utf8' }));
    const typeDefsNotifications = gql(fs.readFileSync('./src/schema/notifications/schema.graphql', { encoding: 'utf8' }));

    const resolversCourses = require('./src/schema/courses/resolvers');
    const resolversDepartments = require('./src/schema/departments/resolvers');
    const resolversEmployees = require('./src/schema/employees/resolvers');
    const resolversNotifications = require('./src/schema/notifications/resolvers');

    const server = new ApolloServer({
        typeDefs: [
            typeDefsCourses,
            typeDefsDepartments,
            typeDefsEmployees,
            typeDefsNotifications
        ],
        resolvers: merge({},
            resolversCourses,
            resolversDepartments,
            resolversEmployees,
            resolversNotifications
        ),
        dataSources: () => {
            return {
                coursesService: new CoursesService(process.env.COMPANY_COURSE),
                departmentsService: new DepartmentsService(process.env.COMPANY_DEPARTMENT),
                employeesService: new EmployeesService(process.env.COMPANY_EMPLOYEE),
                notificationsService: new NotificationsService(process.env.COMPANY_NOTIFICATION)
            }
        },
        plugins: [ApolloServerPluginLandingPageGraphQLPlayground]
    });

    await server.start();

    server.applyMiddleware({ app });

    await new Promise(resolve => httpServer.listen({ port: app.get('port') }, resolve));

    console.log(`🚀 Server ready at http://localhost:${app.get('port')}${server.graphqlPath}`);
})();