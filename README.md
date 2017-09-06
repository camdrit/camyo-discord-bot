# Camyo Discord Bot
Camyo is an LGBT friendly Discord bot made using [Discord.Net](https://github.com/RogueException/Discord.Net) (along with Newtonsoft's [JSON.NET](https://www.newtonsoft.com/json) and [FleuntScheduler](https://github.com/fluentscheduler/FluentScheduler)).

# Features
* ```~pronouns me|he|she|they``` The ability to remember a user's pronouns. The bot will set pronouns for a user on command and only refer to that user using their set pronouns.
* ```~birthday M/D/YY``` The ability to remember a user's birthday. The bot will set a user's birthday on command. The bot checks for birthdays once per day at noon time (local system time) by default. If there are birthdays on the next day, the bot will signal @here reminding other users about the birthdays.
* ```~validate user``` Sends a randomly chosen bit of encouragement about the specified user to chat.
* ```~clean #``` Cleans a max of 100 messages in the chat. Only removes the bot's messages and messages sending commands to the bot. Requires moderator permissions to the channel.

All commands can be used with a mention to the bot instead of the ~ prefix as well.

# Configuration
* To run the bot on your server you'll need to provide a bot token and a primary channel ID (specifies where birthday announcements are made) in the app.config (camyo.dll.config after building) file.

# Builds
You can download pre-built versions of the bot from my website:

* [Lite](https://developedbycam.net/storage/camyo-0.0.9a.zip) (Requires .Net Core to be installed on your system)
* [Full](https://developedbycam.net/storage/camyo-0.0.9a-full.zip) (Requires no extra dependencies to be installed)

# Building
Before building from source be sure to install the following NuGet packages:

* Discord.Net 
* Discord.Net.Commands
* Discord.Net.Core
* Discord.Net.WebSocket
* FluentScheduler
* Newtonsoft.Json

# TODO
* Better logging via Discord.Net's Log task
* Loading validations from a json file so that more can be added dynamically after run-time
