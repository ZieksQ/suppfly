# Suppfly: B2B E-Commerce System

A **B2B** system for manufacturers, wholesalers, and distributors that offers bulk buying of products and services. This e-commerce platform helps your business grow.

### About the Name
i'm still undecided about the name of the project, many of my ideas for naming has already exsit or taken. like *CartGo* which is like cargo but can also mean *cart* and *go*. *hako* (**箱**) which means *box* in japanese, is also been taken by a real business. **Suppfly** means **supply** and **fly**, I want it to sounds like fast and can ship products to different countries so **fly** is like *fast* and *travel* in diffrent places, it is already been taken by local or online business but will use this as temporary name for the project. will change the project name during deployment.

## Tech Stack

- **FrontEnd**: Next.js, Tailwind, Shadcn
- **BackEnd**: ASP.NET Core
- **DB**: PostgreSQL, SQL Server(alternative)
- **ORM**: EF Core
- **Security**: Identity, JWT Auth, RBAC
- **Planning**: MediatR, CQRS, TanStack Query, Fluent Validation, Redis

## Architecture

- **MVP**: Vertical Slice Architecture (api)
- **UI**: Next.js (SSR + CSR *dashboard*)
- **Repo**: MonoRepo
- **Planning**: Modular Monolith w/ Vertical Slice Architecture

## System Flow

1. Onboarding → Company applies → Admin approves → Users invited under company account
2. Browsing → User logs in → Sees company-specific catalog & pricing → Adds to cart
3. Ordering → Cart submitted → Approval workflow (if required) → Order confirmed → ERP synced
4. Fulfillment → Warehouse picks/packs → Shipped → Tracking updated → Invoice generated
5. Payment → Net terms invoiced → Payment recorded → Credit limit updated

## Features (MVP)
<details>
<summary>Account & Access</summary>

- Company account registration with admin/sub-user roles
- Approval workflow for new accounts
- Role-based permissions (buyer, approver, admin)
</details>
<details>
<summary>Catalog & Pricing</summary>

- Product catalog with bulk/tiered pricing
- Customer-specific or contract pricing
- MOQ (minimum order quantities) enforcement
- Product availability by account or region
</details>
<details>
<summary>Ordering</summary>

- Quick order / CSV bulk upload
- Saved order templates / reorder from history
- Multi-approval purchase workflows
- Draft/saved carts per user
</details>
<details>
<summary>Payments & Credit</summary>

- Net terms (Net 30/60/90) support
- Credit limit management
- PO number capture at checkout
- Invoice generation & download
</details>
<details>
<summary>Shipping & Fulfillment</summary>

- Multiple ship-to addresses per account
- Freight/LTL shipping options
- Estimated delivery dates
- Order tracking
</details>
<details>
<summary>Account Self-Service</summary>

- Order history & status
- Invoice & payment history
- Returns/RMA requests
- Account statements
</details>
<details>
<summary>Integration (table stakes for B2B)</summary>

- ERP sync (inventory, pricing, orders)
- Basic API for EDI/procurement systems
</details>

