FROM node:8 AS build-env
WORKDIR /build
COPY package*.json ./
RUN npm install
COPY . .
RUN ls -R
RUN npm run build

FROM nginx
COPY --from=build-env /build/nginx.conf /etc/nginx/conf.d/
COPY --from=build-env /build/dist/ /usr/share/nginx/html
