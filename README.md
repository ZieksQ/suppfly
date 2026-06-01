# Supply: B2B E-Commerce System

A one stop shop for bulk buying products. **business-to-bussiness** e-commerce help your bussiness grow.

## Tech Stack

**FrontEnd**: Next.js, Tailwind, Shadcn
**BackEnd**: ASP.NET Core
**DB**: PostgreSQL, SQL Server(alternative)
**ORM**: EF Core
**Security**: Identity, JWT, RBAC
**Planning**: MediatR, CQRS, TanStack Query

## Architecture

**MVP**: Vertical Slice Architecture (api)
**Repo**: MonoRepo

## System Flow

```ts
Registration: client -> register business -> confirmation process -> created company account
Shopping: client login -> browse -> add to cart -> submit cart -> request review -> order created
Tracking: admin login -> track orders ...
```


