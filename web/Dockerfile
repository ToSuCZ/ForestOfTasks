# ---- Stage 1: Build the Next.js app ----
FROM node:22-alpine AS builder

# Set working directory
WORKDIR /app

# Copy package.json and yarn.lock for dependency installation
COPY package.json yarn.lock ./

# Install dependencies using Yarn
RUN yarn install --frozen-lockfile

# Copy the rest of the app
COPY . .

# Build the Next.js application
RUN npx prisma generate && yarn build

# ---- Stage 2: Production Image ----
FROM node:22-alpine

# Set working directory
WORKDIR /app

# Copy built files from the builder stage
COPY --from=builder /app ./

# Expose port (Next.js defaults to 3000)
EXPOSE 3000

# Set environment variables for Next.js
ENV NODE_ENV=production
ENV NEXT_PUBLIC_API_URL=http://localhost:5000

# Start the Next.js app using Yarn
CMD ["yarn", "start"]