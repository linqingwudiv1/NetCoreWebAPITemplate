###Redis

``shell

docker run -d --name redis_IDGenerator -p 6379:6379 -v /data/lq/redis-data/redis.conf:/etc/redis/redis.conf -v /data/lq/redis-data/data/:/data redis --requirepass 1qaz@WSX --appendonly yes		
--restart=always
``

# PGSql

``shell

docker run -d --name pgsqldb -p 5432:5432 -e POSTGRES_PASSWORD=1qaz@WSX -v /data/lq/pgsql-data:/var/lib/postgresql/data  postgres 
--restart=always
``

#Nginx


#RabbitMq

``
docker run -d --restart always --name rabbitmq -p 5672:5672 -p 15672:15672 -p 25672:25672 -v /data/lq/rabbitmq-data/data:/var/lib/rabbitmq rabbitmq

-p 指定端口映射，此处 5672 端口用于程序访问 RabbitMQ 的接口；15672 端口是用于 RabbitMQ 可视化 UI 管理的暴露端口，可以通过在浏览器中输入 localhost:15672 访问 RabbitMQ 的管理控制台，默认登陆账号密码是 guest,guest；25672 端口用于 RabbitMQ 集群各节点之间的通讯。
-v 将本地目录挂载到 Docker 容器中以实现数据持久化，用法：-v local_dir:docker_dir, 路径使用绝对路径
``