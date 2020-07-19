# Redis In Docker

> [Redis 官方](https://github.com/redis/redis)不支持Windows编译，微软虽然维护了[Windows的版本](https://github.com/microsoftarchive/redis)，但活跃程度肯定不如Redis官方，因此使用运行在Docker的Redis服务器，而不需要自己编译和执行Redis



# 安装 Docker Desktop for Windows

​	进入 Docker Desktop 官方网站，并下载安装包：

> https://www.docker.com/products/docker-desktop

​	注册Docker的账户并登录在 Docker Desktop



# 下载 Redis 镜像

> 国内访问Docker镜像会存在网络问题，需要给Docker配置国内可用的镜像地址：
>
> https://{--------}.mirror.aliyuncs.com

​	在 Docker Hub 搜索 Redis 关键字，找到Redis官方提供的 Redis Docker 镜像：

> https://hub.docker.com/_/redis

​	在CMD运行命令以拉取镜像：

```
docker pull redis
```



# 创建 Redis 容器

​	在CMD执行命令创建容器：

```
docker run --name asp-redis -it -d redis
```



# 启动 Redis 容器及CLI

​	此时在 Docker Desktop 即可看到正在运行的 asp-redis 容器，执行以下命令启动Redis的CLI：

```
docker exec asp-redis -it redis-cli
```

# 测试 Redis

​	执行以下命令测试Redis功能：

```
help
set leon 100 EX 10
get leon
```

