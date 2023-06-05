# Documentation
## Introduction
The goal of this project was to adhere to both the guidelines of the assignment and the spirit of what I know about Farallon's internal
tech stack. The UI is a WinForms application targeting the .net framework 4.7.2 runtime. I allowed myself only one Nuget package- NewtonSoft-
for deserializing JSON.

## Code Style
### Class Layout
I try to structure my classes to follow this pattern, from top to bottom:
- private constants
- private statics
- private readonly members
- private members
- public properties
- constructors
- public methods
- private methods as close to where they are used without ever being above where they're referenced

### Naming Conventions
Projects try to follow the pattern of [Domain].[Concern]

### 'var' vs. type
I don't have strong feelings on 'var' vs. explicit typing. I gravitate towards 'var' for the simplicity and agility in prototyping, but try to use
the explicit type when it would help to convey non-obvious usage. So you're likely to see a blend of the two in my code. In an enterprise codebase my standard is
'whatever the organization consensus is'.

### Terminology
My guidelines for naming data transfer classes in this project broadly follows:
- Record, a class representing information read from a persistant data store
- Contract, a class representing data bound from an external service such as a REST API
- Model, a class used to represent data related to presentation concerns

## UI and UX
The interface of this application practices a minimalist approach to functionality due to my lack of familiarity with the finance world. My reasoning is that attempting to add
bells and whistles for a domain I don't fully understand, without proper use-cases, is likely to produce something unhelpful and unusable. Instead time was spent building out
the patterns and design rails for future extensions.

## Architecture

### Model-View-Presenter
The structure of this solution roughly follows a 'model-view-presenter' architecture. Component-layer services are entirely isolated from the rendering 'host' by
the presentation layer.

Components on the host layer implement view interfaces, which is what binds them to their presenter. The presenter acts as a mediator between component pieces needed to
drive the UI. Interactions between a view implementation and a presenter take the form of 'call' and 'response' invocations.

No component layer types are permitted to be referenced in the host layer. Types must be translated to an equivalent view model. This separation ensures the form component
layer data takes can evolve independently from the presentation layer. As a rule, view models should be flat POCO types whenever possible.

The view implementation essentially 'asks' the presenter to provide it with something. Rather than providing it as a return parameter, the presenter invokes a view method.
This allows orchestration logic to remain centralized in the presentation layer. A benefit of this separation is being able to more readily swap out the host layer with a new framework.

It is also the responsibility of the presentation layer to drive what the view interface will ultimately display- meaning it handles text formating, ordering or filtering of records,
and localization of string constants if relevant.

The MVP architecture does have some downsides:
- It's not a perfect fit for WinForms' single-tenant UI threading model
- Due to cyclical references, a view interface cannot be injected into a presenter if the concretion has a presenter injected into it
- Mapping component layer models to presentation layer equivalents can create duplication and dual maintainence concerns

Although this pattern has worked well in the WebForms world, with the benefits of hindsight and more time I would probably opt to pursue something more similar to a MVVM architecture,
maybe drawing inspiration from how modern single-page applications use a rendering tree.

### Functional-lite Concepts
While I can't pretend to be anything more than a layman, there are aspects of functional programming I've found to work well when building out components with consistent, reliable,
behavior. Specifically, a monad-esque 'Result<TType>' to wrap around function calls and model operation call success states. It's not a pattern I _need_ to use, but in my experience
 it helps with predictability and controlling application flow for outside parties consuming your component.

### Eventing
UI interactions are loosely-coupled through an event-driven system and the injection of a 'state accessor'. The state accessor acts as a state machine and invokes registered Actions
when the associated field (currently only 'active portfolio') is updated. This helps to prevent having to spread direct references to sibling or parent components. The backend
eventing implementation supports both synchronous and asynchronous event calls. Note that it shares the same limitation as any other threading implementation in WinForms- modifying
the UI should only happen from the UI thread.

This is supported by having view implementations' methods invoked by their presenters only update their internal state and doing any UI changes on a 'done changing' callback. This approach
is partially inspired by modern web frameworks' use of a 'dirty' flag for signalling that a component needs to redraw itself.

### Error Handling
Error handling in this application happens at the presentation layer. Calls into components that might trigger an unhandled exception are wrapped in the monadic Result.Try method.
The rationale for this approach is the philosophy that how to respond to an error varies from host to host. It doesn't make sense to crash a user-facing application, but the severity
of an external API not being available may be different for a nightly processor.

### Utilities
I've grouped concepts that would normally be a platform-wide concern based on organizational standards into a project called 'Utilities' to avoid adding a bunch of assemblies
with a few classes each to such a small solution. Normally I would consider such a broad classification of functionality an anti-pattern, or at least a risk of adding kludge
into a monolithic codebase.

### Dependency Injection
I wanted to demonstrate the principles of true inversion-of-control without relying on a 3rd party package. The Utilities project includes a very slim dependency container implementation
roughly modeled after the Microsoft.Extensions.DependencyInjection platform library. It should not be considered anything more than a novelty.

### HTTP Client
Similarly to dependency injection, I didn't want to rely on a 3rd party package. However, I also didn't want to use the types built into the .net framework runtime due to a lack of
portability. As a compromise, the Utilities project contains abstractions for wrapping around and injecting a concrete client type. This pattern would allow for (hypothetically) seamless
replacement as part of a modernization effort from framework to core/.net5+

### JSON
Neither Newtonsoft nor System.Text.Json allow for injecting a serializer through an interface. Ostensibly this is usually going to be fine, but I personally prefer avoiding static 'helper'
functionality when possible.

### Logging
There's a distinct lack of logging throughout the project. Unfortunately this was scope cut for time constraints.

## Opportunities
- Logging, including a log sink popup that allows for filtering messages by category and level
- Real-time quotes from 3rd party APIs
- Refactoring the UI to a component tree model
- Localization
- Refactoring eventing, currently ALL 'on changing' events must complete before the 'done changing' events fire