# FamMan Documentation Docsify Wiki

This directory contains the Dockerfile and setup for a local, hot-reload Docsify wiki serving the documentation in `docs/`.

## Features
- Auto-generated navigation from markdown structure
- Dark theme by default
- Plugins: copy code, zoom image, flexible alerts, tabs, themeable, sidebar collapse, mermaid, search
- Custom title: "FamMan Documentation"

## Usage

1. **Build the Docker image:**
   ```sh
   docker build -t famman-docsify ./docsify
   ```
2. **Run the container (serves docs on http://localhost:3000):**
   ```sh
   docker run --rm -it --name famman-docsify -p 3000:3000 famman-docsify
   ```


The container serves the documentation with hot-reload using a static `index.html` in `docs/`.

## Directory Structure
- `docs/` — Markdown documentation (copied from project root docs/)
- `Dockerfile` — Container build instructions
- `entrypoint.sh` — Starts Docsify

## Customization
- To update docs, edit files in `docs/` and refresh the browser.
- To change plugins, theme, or site configuration, edit the `index.html` file in `docs/`.

---

For more information on Docsify plugins, see: https://docsify.js.org/#/plugins
