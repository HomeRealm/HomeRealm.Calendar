# Dockerfile for FamMan Docsify Wiki

FROM node:20-alpine

WORKDIR /app

# Install Docsify CLI globally
RUN npm install -g docsify-cli

# Install required plugins via npm (for local dev, but most plugins are loaded via CDN)
RUN npm install docsify-themeable --save

# Copy entrypoint script
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Copy documentation
COPY docs /docs

EXPOSE 3000

ENTRYPOINT ["/entrypoint.sh"]
