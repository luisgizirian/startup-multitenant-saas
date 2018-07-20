# Multitenant SaaS Blocks for Startups

Guidance for a low-to-grow-budget startup on the SaaS space to build its foundational bits :)

## First, a word or two on multitenant
The concept means different things to different people. What is the scenario we're talking about when refering to it?

Our take is when a provider allows usage of its plaform to a subscriber, whom in turn, offer services on top it for its own customers, under his branding/name (so, as if it was his own platform). This requires a higher separation degree (i.e. users, site branding). The SaaS provider has a couple options to build such offering:

1. Build a separate tenant (platform instance) per customer.
2. Co-exist multiple tenants on top of the platform, providing isolation and some sort of customization, although using shared components and (optionally) infrastructure.

We pursuit the second optiondd, which allows for better maintainability and scaling.

## Some premises
* Thought for low-to-grow-budget. Startup mentality.
* Replaceable pieces. No ties.
* Avoid Cloud provider lock in.
* All used bits OSS.

*[TODO: review initial list]*

## Composed of
Here's a list on components that build this up.

* Identity Service
* Tenant Configuration Service
* Multitenant Service

*[TODO: review initial list]*

## Implementations
Once the services are in place, they have to operate on a daily basis. As with most things, options...

* Containerized on [Kubernetes](https://github.com/kubernetes/kubernetes)
* Microservices on [Service Fabric](https://github.com/Microsoft/service-fabric)
* Single VM environment
* Actor model based form (*[define]*)

## Run on

* Local development
* Public Cloud provider (Anyone. Really. It's up to you.)

## Guidance tech stack...

### On [.NET Core](https://github.com/dotnet/core)
Modern, runs everywhere, plays well with others and it's performant. Quite a combo to lay foundations for whatever SaaS you're planning to build. Right?

### On [Identity Server](https://github.com/IdentityServer/IdentityServer4)
We picked this OSS project to buid upon our Identity Service, as it fits both our goals and underlying tech. So, we feel comfortable using it :)
