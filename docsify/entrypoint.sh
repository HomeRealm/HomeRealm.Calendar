#!/bin/sh
set -e


# Auto-generate a nested _sidebar.md reflecting the directory structure

generate_sidebar() {
	local dir="$1"
	local indent="$2"
	local sidebar="$3"
	for entry in $(find "$dir" -mindepth 1 -maxdepth 1 | sort); do
		if [ -d "$entry" ]; then
			local folder_name=$(basename "$entry")
			echo "${indent}* **${folder_name}**" >> "$sidebar"
			generate_sidebar "$entry" "  $indent" "$sidebar"
		elif [ -f "$entry" ] && [[ "$entry" == *.md ]] && [[ "$entry" != */_sidebar.md ]] && [[ "$entry" != */README.md ]]; then
			local file_name=$(basename "$entry")
			local display_name="${file_name%.md}"
			local rel_path=${entry#/docs/}
			echo "${indent}* [${display_name}](/${rel_path})" >> "$sidebar"
		fi
	done
}

SIDEBAR_FILE="/docs/_sidebar.md"
echo "* [Home](/README.md)" > "$SIDEBAR_FILE"
generate_sidebar "/docs" "" "$SIDEBAR_FILE"

# Start Docsify dev server
exec docsify serve /docs --port 3000
