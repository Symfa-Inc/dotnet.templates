FROM node:16.14-alpine3.14 as builder
RUN mkdir -p /app
WORKDIR /app

#copy and install node_modules
COPY package.json .
RUN yarn install --production

WORKDIR /app
COPY . .

RUN yarn build

FROM nginx:1.15.8
COPY --from=builder /app/build /usr/share/nginx/html
COPY ./vhosts.conf /etc/nginx/conf.d/default.conf

# CMD sed -i -e 's@__REACT_APP_API_URL@'"$REACT_APP_API_URL"'@g' /usr/share/nginx/html/static/js/*.js && \
# nginx -g "daemon off;"

CMD nginx -g "daemon off;"