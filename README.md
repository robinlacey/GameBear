# :floppy_disk::bear:

**Game Bear** (:floppy_disk::bear:) stores game data. Game Bear is used by Dealer Bear (🎰:bear:) to save and update in-progress games through a `Session ID`

It's not a very clever bear and currently just CRUDs all data through an in-memory gateway. I



Game Bear uses `MassTransit` with `RabbitMQ` by default. This could be easily changed by creating a different `IPublishMessageAdaptor `

- Set the `RABBITMQ_HOST` environment variable along with `RABBITMQ_USERNAME` and `RABBITMQ_PASSWORD` 
- Build and run the `Dockerfile `



**Warning**: :floppy_disk::bear: is still work in progress and is missing some key functionality. Feel free to drop me a line if you have any questions.

