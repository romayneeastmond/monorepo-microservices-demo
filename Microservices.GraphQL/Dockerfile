FROM  node:14-alpine
WORKDIR /app
EXPOSE 4000

COPY ["Microservices.GraphQL/package.json", "."]
COPY ["Microservices.GraphQL/package-lock.json", "."]
COPY ./Microservices.GraphQL ./

RUN npm i

CMD ["npm", "run", "apollo-server"]