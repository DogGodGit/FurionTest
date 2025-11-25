# FurionTest 项目

## 项目简介
FurionTest 是一个基于 **.NET 8** 和 **Furion 框架** 的综合性 Web 项目，旨在快速开发高效、模块化的企业级应用。项目支持 Web API、Razor Pages 和后台任务处理，集成了强大的数据库操作和单元测试功能。

## 功能特点
- **Web 支持**：
  - 提供 Web API 和 Razor Pages 支持。
  - 使用 `Furion.Pure` 框架，简化开发流程。
- **数据库操作**：
  - 集成 `SqlSugar` ORM，支持多种数据库操作。
  - 配置了全局 SQL 日志拦截和错误处理。
- **后台任务**：
  - 包含 Worker Service 项目，用于运行后台任务或定时作业。
- **单元测试**：
  - 使用 `Furion.Xunit` 提供单元测试支持，确保代码质量。
- **敏感词过滤**：
  - 嵌入了 `sensitive-words.txt` 文件，可能用于敏感词过滤功能。

## 项目结构
- **FurionTest.Web.Entry**：
  - 项目入口，包含 Web API 和 Razor Pages 的实现。
- **FurionTest.BAS**：
  - 基础服务层，包含业务逻辑。
- **FurionTest.Web.Core**：
  - 核心功能模块，提供通用工具或核心逻辑。
- **TestProject2**：
  - 单元测试项目，验证项目功能的正确性。

## 环境要求
- **.NET 8 SDK**
- **Furion 框架**（版本 4.9.0）
- **SqlSugar ORM**

## 快速开始
1. **克隆项目**：
	git clone <repository-url> cd FurionTest
2. **安装依赖**：
	确保已安装 .NET 8 SDK，并运行以下命令：
	dotnet restore
3. **运行项目**：
   进入 `FurionTest.Web.Entry` 目录，运行以下命令启动项目：
   dotnet run
4. **运行单元测试**：
	进入 `TestProject2` 目录，运行以下命令执行测试：
	dotnet test

## 配置说明
- **数据库配置**：
- 数据库连接配置在 `GlobalConfig.ConnectionConfigs` 中。
- 支持 SQL 日志拦截和错误处理，便于调试。
- **敏感词文件**：
- `sensitive-words.txt` 文件嵌入为资源文件，可用于敏感词过滤功能。

## 贡献指南
欢迎提交 Issue 和 Pull Request 来改进项目。请确保在提交代码前运行所有单元测试。

## 许可证
该项目遵循 [MIT 许可证](LICENSE)。