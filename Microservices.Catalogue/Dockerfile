FROM  node:14-alpine
WORKDIR /app
EXPOSE 3000

COPY ["Microservices.Catalogue/package.json", "."]
COPY ["Microservices.Catalogue/package-lock.json", "."]
COPY ./Microservices.Catalogue ./

RUN npm i

CMD ["npm", "run", "start"]