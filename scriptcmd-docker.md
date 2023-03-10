# Redis

``shell
docker run -d --restart always --name redis_IDGenerator \
           -p 6379:6379 \
           -v /data/redis-data/redis_IDGenerator/redis.conf:/etc/redis/redis.conf \
           -v /data/redis-data/redis_IDGenerator/data/:/data redis --requirepass 1qaz@WSX --appendonly yes
           
``

# PGSql

## 修改pg_hba.conf文件
``
# IPv4 local connections:
# 新增这行表示允许任意ip连接到PGSQL
host  all    all    0.0.0.0/0    md5

``
docker run -d --restart always --name pgsqldb \
           -p 5432:5432 \
           -e POSTGRES_USER=postgres \
           -e POSTGRES_PASSWORD=1qaz@WSX \
           -v /data/pgsql-data:/var/lib/postgresql/data \
           postgres:latest 
``

# Nginx

### 最简单的nginx
``
docker run -d --name nginx-simple -p 9301:80 nginx:latest
``
### 可用
``
docker run -d --restart always --name nginx-main \
           -p 8080:80 \
           -v /data/nginx-data-main/nginx.conf:/etc/nginx/nginx.conf:ro nginx:latest
``
# RabbitMq

``
docker run -d --restart always --name rabbitmq \
           -p 5672:5672 \
           -p 15672:15672 \
           -p 25672:25672 \
           -e RABBITMQ_DEFAULT_USER=guest \
           -e RABBITMQ_DEFAULT_PASS=1qaz@WSX \
           -v /data/rabbitmq-data/data:/var/lib/rabbitmq rabbitmq:3-management
``

-p 指定端口映射，此处 5672 端口用于程序访问 RabbitMQ 的接口；15672 端口是用于 RabbitMQ 可视化 UI 管理的暴露端口，可以通过在浏览器中输入 localhost:15672 访问 RabbitMQ 的管理控制台，
默认登陆账号密码是 guest,guest；
25672 端口用于 RabbitMQ 集群各节点之间的通讯。
-v 将本地目录挂载到 Docker 容器中以实现数据持久化，用法：-v local_dir:docker_dir, 路径使用绝对路径
