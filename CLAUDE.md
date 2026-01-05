# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**好来运智能仓储管理系统 (HLY WMS)** - A comprehensive Warehouse Management System built with ASP.NET WebForms (.NET Framework 4.6).

- **Solution File**: `House/House.sln`
- **Startup Project**: `Cargo` (located at `House/Cargo/Cargo/`)
- **IDE**: Visual Studio 2022

## Build Commands

```bash
# Build solution from command line
msbuild House/House.sln /p:Configuration=Release

# Build specific project
msbuild House/Cargo/Cargo/Cargo.csproj /p:Configuration=Debug

# Restore NuGet packages
nuget restore House/House.sln
```

For development, open `House/House.sln` in Visual Studio and press F5 to run with IIS Express.

## Architecture

The system uses a classic layered architecture:

```
Cargo (Web) → House.Business → House.Manager → House.Entity → House.DataAccess → SQL Server
```

### Project Dependencies
| Project | Type | Purpose |
|---------|------|---------|
| **Cargo** | Web | Main WMS web application (startup) |
| **House.Business** | Class Library | Business logic layer |
| **House.Manager** | Class Library | Data management layer |
| **House.Entity** | Class Library | Entity models |
| **House.DataAccess** | Class Library | Database access (ADO.NET wrapper) |
| **House** | Web | Secondary web module |
| **Dealer** | Web | Dealer management portal |
| **Supplier** | Web | Supplier management portal |
| **HLYEagle** | Web | HLY Eagle system |
| **vMallDLT** | Web | E-commerce module |
| **HouseServices** | Service | Background services |

## Key Patterns

### Frontend
- **EasyUI**: jQuery EasyUI framework for UI components (datagrid, panel, linkbutton, etc.)
- **Pattern**: Pages use `easyui-*` CSS classes and data-options attributes
- **Navigation Buttons**: Use `class="easyui-linkbutton" iconcls="icon-*"` pattern
- **Current Page Highlight**: Active navigation button uses `style="color: Red;"`

### Backend
- **Page Base Class**: Pages inherit from `BasePage` in `Common/BasePage.cs`
- **API Pattern**: API endpoints typically in `*Api.aspx` files (e.g., `orderApi.aspx`, `houseApi.aspx`)
- **Logging**: Use `Common.WriteTextLog(message)` - logs to `System/Log/yyyy-MM-dd.System.txt`
- **Configuration**: Settings in `Web.config`, accessed via `ConfigurationSettings.AppSettings["key"]`

### Caching
- **Redis**: Token management, session sharing (prefix: `DLQF`)
- **Memcached**: Hot data caching
- Helper class: `Common/RedisHelper.cs`

## Important Directories

```
House/Cargo/Cargo/
├── House/          # Warehouse management pages
├── Order/          # Order management pages
├── Product/        # Product management
├── Report/         # Report pages
├── Finance/        # Financial management
├── Client/         # Customer management
├── Interface/      # API interfaces
├── Api/            # REST API endpoints
├── Weixin/         # WeChat integration
├── Common/         # Shared utilities
└── System/         # System admin & logs
```

## Database

- **Database**: SQL Server
- **Access Pattern**: ADO.NET via `House.DataAccess` layer
- **Key Classes**: `Database.cs`, `DatabaseFactory.cs`, `DataAccessor.cs`
- **Scripts Location**: `SQL脚本/` folder in root

## Third-Party Integrations

The system integrates with:
- WeChat (Service Account, Mini Program, Enterprise WeChat)
- WeChat Pay, Alipay
- Continental (马牌) tire system
- GTMC order system

## Code Conventions

- **Language**: Code comments and UI text are in Chinese
- **Naming**:
  - Pages: PascalCase with `.aspx` extension
  - Entities: Match database table names
  - Managers: `*Manager.cs` pattern
  - Business: `*Business.cs` pattern
- **Encoding**: UTF-8 with BOM for `.aspx` files

## Reference Documentation

See `WMS架构文档.md` in the repository root for comprehensive architecture documentation including:
- Complete module descriptions
- Third-party integration details
- Business process flows
- Database design overview
