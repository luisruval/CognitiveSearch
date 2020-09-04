# Instructor Notes

This folder has resources for the instructor listed below.

## Opening Presentation

Use the KMB-Opening.pptx presentation to:

1. Presenters name
1. Start your training
1. Polls
1. Customized Agenda
1. Announcements
1. Known bugs not fixed yet
1. CSW x KMB difference (slide 4)
1. Breaking News
1. Spektra Instructions
1. Github link - <http://aka.ms/kmb> or <http://aka.ms/LearnAI-kmb>
1. Survey URL
1. Talk about environment cleaning, to avoid costs.

While the introduction has a pdf version, this one won't have one. The idea is to keep on editing this presentation for each delivery.

## Azure Cognitive Search LAB

Open the page <https://alice.unearth.ai/> to show how important is the Analyzer definition. This page shows the different results you can get from Standard Lucene, English Lucene and English Microsoft. Search for:

+ "Alice": Standard Lucene gets 10 hits less than the others
+ "Thinking": Now the hits totals are very different. English Lucene returns "think" too. English Microsoft returns even"thought"
+ "Knives": Only Microsoft English returns information, where "knife" was found

## Text Skills LAB

+ Important to read the skills been used with the students, explain why pages with 4000 characters is an example of valuable information that teaches how text split is important. It also helps to learn how the output of one skill is input of another one.
+ Some warnings about long words and/or text are expected, Cognitive Search works with the limitations of the Cognitive Services APIs that are used under the hood.
+ Someone may ask why there are duplicates for `keyPhrases` and `organizations`. It is unique per page, we are creating lots and lots of them, because on the Entity Recognition actual (December 2018) 4000 characters limit. All of it may change in the future, the limits and the uniqueness per page.

## Image Skills LAB

+ OCR: Expected performance is 3 seconds per image.  For documents with multiple images, you can apply those 3 seconds for every one of them. The product performance is under constant optimizations and improvements are expected soon
+ Talk about normalized images. The lab has a "Note!" for that. All info you need is there
+ Highlight that now the skillset is merging and splitting the content

## Custom Skills LAB

Important to comment in the Azure Functions Code:

1. language: eng (Content Moderator) x en (Azure Cognitive Search)
1. For now, language is not dynamic (not using the document extracted language). So, we are not moderating spanish docs very well.
1. 1024 limit.
1. host x region. Without and with https
1. Complex types explanation: why we are transforming in boolean
1. You can use [this](https://github.com/Rodrigossz/AzureCognitiveSkill) Azure Functions solution to demo the complex type that is returned
1. Explanation: we are only using PII

Important to comment about the skillset:

1. We could submit entities and organizations to content moderator
1. Content Moderator limit, 1024 characters, could be a smaller problem if we submit the pages instead of the merged text. Maybe in the next version of the training. You can ask those who finished early to do it. Extra Challenge.

## Bot LAB

+ You can demo searches for "linux" and "LearnAI". Ctrl+click the URL to open the IMAGES, not the other files.
+ Search for moderated documents
+ Explain details of Framework 4 code

## Final Case - Finished Solution

Expected:

+ Complex Scenarios
  + Multiple datasets:
    + More than one datasource: product catalog, reviews, ERP, CRM
    + Data source is too big, data needs to be partitioned
    + Datasources updated in different times
  + Multiple Indexes
    + One for each region, in each languages
    + Each region demands different fields or ranking or Analyzer or suggester
  + Multiple Skillsets
    + Each product or region requires different schedules
    + Partition the data in 2: images and not images, to save processing time and money with specific skillset for each type of data.
    + External data coming from VIVINO/Logistics/Other custom skillset with a different schedule update - "runs once a day"

+ Sizing discussion: [Tier](https://azure.microsoft.com/en-us/pricing/details/search/) x [SLA](https://azure.microsoft.com/en-us/support/legal/sla/search/v1_0/), Units. For SLA is required 2 or more copies. Basic tier limit is 2 GB, Standard has 300 GB total. **Also, it is required at least Basic tier to have replicas. And the replica can be located in the Brazil South region, reducing latency for South America clients.**

+ Skills
  + Text Skills
  + OCR (text in images)
  + Image Analysis too (images scenes descriptions, Celebrity Detection)
  + Language detection
  + Entity Extraction (location, organizations)
  + Important to discuss the order of the transformations

+ Other Data & AI that could be used
  + Translation API
  + Personalization API (Private Preview in Jan 2019)
  + Entity Linking API for the detected entities
  + Bing Search in the Bot to improve the search experience
  + CosmosDB as a data source for the product catalog
  + AML for custom AI for published as an API for: price optimization, campaign optimization, etc

+ South America means 2 more languages after the original docs in English: Portuguese and Spanish. It is expected discussions like:
  + Save all fields in 3 languages? Storage costs. Write once and read many
  + 1 index in BR South Region with fields in Portuguese and Spanish + 1 index in the US in English
  + Translate on the fly? Data out and API usage costs, latency