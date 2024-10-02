# MacysScrapperAPI
Project implementing an API for scraping data from Macys webstore item pages. 

## API Information
### Example CURL:
- curl -X 'POST' \
  'https://localhost:7148/api/Scraper' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '[url]'
### Example response JSON
<code>{
  "name": "string",
  "images": [
    "string"
  ],
  "brandName": "string",
  "price": 0,
  "discount": 0,
  "ratings": "string",
  "description": "string",
  "bulletPoints": [
    "string"
  ],
  "attributes": {
    "colors": [
      "string"
    ],
    "sizes": [
      "string"
    ]
  }
}</code>
