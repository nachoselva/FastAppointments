# Stage 1: Dev
FROM node:22-bullseye AS development
WORKDIR /app
COPY package.json ./
COPY package-lock.json ./

RUN npm install --include=dev

COPY . .
EXPOSE 5173
CMD ["npm", "run", "dev"]

# Stage 2: Build
FROM node:22-bullseye AS build
WORKDIR /app
COPY package.json ./
COPY package-lock.json ./

RUN npm install

COPY . .
RUN npm run build

# Stage 3: Production
FROM nginx:bullseye AS production
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]