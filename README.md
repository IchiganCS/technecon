# Technecon

The source code for <https://technecon.de>!

## Data layout

- `Artists`
  - `Sex`: 0 is male, 1 is female
  - `Occupations`: 1 is author, 2 is painter, 4 is composer - for now, only one of those is possible
  - `Commonfirstname`: only has to be specified if there actually is a common firstname. If `Allfirstnames` is good enough, leave `Commonfirstname` empty.
  - Variable pointing to a file (`markdown` and `picture`) can be converted to a readable file from the disk via `Url.Content($"wwwroot{path}")`.
  - `birthday` and `obit` point to a specific day.
  - The given `picture` has to have a 512x512 resolution.

- `OpusType`
  - 0 is text, 1 is music, 2 is painting

- `Opera`
  - `base` can be specified if the given artwork used an already existing one
  - `path` points to some type of file fitting to the content, e.g. a `.jpg` file for a painting

- The `markdown` file for artists
  - The first line is a quote from the artist.
  - All the following lines are the biography.

- The `markdown` file for an opus
  - Every file needs `---` as a single line
  - The block above is used for a text with no formatting except newlines
  - The block below is a description for anything can be used with regular markdown
