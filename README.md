# Developer Marketplace

Welcome to the Developer Marketplace repository! The Developer Marketplace is a web application that allows individuals, teams and companies share KanBan stories, cards, tasks and items from their KanBan boards and Scrum boards. All shared stories become available on the Developer Marketplace website and software engineers from all over the world can work on these stories. The companies and people that share stories will be called the story producers. The Developer Marketplace will support multiple workflows for transaction and bidding strategies that enable the producers to define the rules under which they will accept bids and allow the story developers to work on their items.

## Developer marketplace Workflow

- A team shares a KanBan story on the Developer Marketplace website
- Developers bid for the story
- The team selects one or more developers to work on their story based on the bids they get.
- The software developers who have won the bid pull the working branch from a code repositor and work on the storyy.
- The team analyzes the pool request and decides wether the code satisfies the initial requirements and accepts it or rejects it.
- If the story is accepted, the team pays the software developer who worked on the story. There are different payment methods, (time, money, stories, no payment required)

To illustrate the site operation better, let's consider the following examples:

### Using time as a currency

- Team "A" has a very interesting software project and the team spends all their time in development but the deadline is near and they need help in order to finish the project on time.
- Let's consider their KanBan board is hosted in Trello.
- Team "A" shares some of their open stories through a special connector that uses the Trello API and they become available on the Developer marketplace website.
- The stories are shared under a special condition that team "A" sets from their Developer Marketplace account. Every developer who wants to work on the shared stories can bid with time (hours/days/weeks/months). Time bidding means that the developer who works on a shared story can require Team "A" to work on his stories as a payback for the time they agreed upon during the bidding process.
- Developer "B" finds the stories that Team "A" shared on the Developer Marketplace and offers a bid of 5 days  to work on their story. (a bid of five days meands that if team "A" accepts the completed story, they owe the developer 5 days of working on his projects)
- The Developer marketplace website tracks the execution and fulfillment of the stories and provides a fair playground.
- Team "A" gets their stories done and meets the deadline. In return they help developer "B" to meet his goals and help with his/hers stories.

### Using money as a currency
- Team "A" shares their KanBan stories using the "Git issues" connector
- Developer "B" provides a money bid to work on one of the Team "A"'s shared stories.
- Team "A" considers the bids and selects Developer "B" to work on their story.
- Developer "B" works on the story.
- Team "A" checks the code of Developer "B" and decides whether to accept his pull request
- Positive scenario: Team "A" accepts the pull request and pays Developer "B" the requested amount using the Developer Marketplace payment processor
- Negative scenario: Team "A" rejects the pull request and can return it to the developer for adjustments or fully cancel the transaction with Developer "B" because of poorly written code. Developer "B" can protest and the Developer Marketplace will arbitrate the matter.
- All Development marketplace decisions are final.

### Using other stories as currency
- Consider the same situation as the previous scenarios but this time Team "A" can work on stories that Developer "B" has posted on the Development Marketplace

### No currency transactions (a.k.a free transactions)
- There is no exchange. The developers work on any free story they choose. The team that shares the story chooses to accept the most suitable pull request.

## Version Changeset
|Version | Changes |Release Type |
|--------|---------|------------:|
|0.0.0   |         |             |

## Developer Marketplace architecture and roadmap
You can find more information about the project roadmap from the ROADMAP.md file.
The architecture diagrams are located in the docs folder.

## Downloading and building the source code
You can clone the repository using the following command:

`git clone https://github.com/cracker4o/dev-marketplace.git`

You can build and run the project either using VS2015 or the Build.ps1 located in the build folder.

## How to contribute
Please read the CONTRIBUTORS.md file to find out more information about how to contribute to the project.
The file contains everything necessary to get you started right away.

## Legal and Licensing

## Project governance

