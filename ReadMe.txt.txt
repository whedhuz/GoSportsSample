Assumptions:
1. Minimal clickable buttons is what we want to achieve due to the employee tech limitation
2. Not all features are implemented, but enough to show what the flow will be

Design decisions:
Using event-driven design because:
1. we can replay and reconstruct a match from events
2. easy to scale up for more event types, matches, and employees

Not using game selection, but instead a URl will be provided to employees to start recording their game.
This way game can be sent to employees via text message/email for easier tracibility too.
Game selection will be an alternative selection in the future


What is lacking:
1. Front UX and usability is not the best
2. Editing event (Update/Insert/Delete) will be harder, and there is no UX for it yet

To run:
1. Run BackEnd project
2. Run Angular project for front end
3. Head to http://localhost:4200/tennis/test_1
