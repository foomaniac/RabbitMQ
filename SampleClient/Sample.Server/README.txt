In this directory you can find an example configuration file for RabbitMQ.

Note that this directory is *not* where the real RabbitMQ
configuration lives. The default location for the real configuration
file is %APPDATA%\RabbitMQ\rabbitmq.config.

%APPDATA% usually expands to C:\Users\%USERNAME%\AppData\Roaming or similar.

Add User:

rabbitmqctl add_user test test
rabbitmqctl set_user_tags test administrator
rabbitmqctl set_permissions -p / test ".*" ".*" ".*"