# graphqldotnet6ApolloAngular

### Learning graphql apollo

Authorization and Authentication
https://stackoverflow.com/questions/53537521/how-to-implement-authorization-using-graphql-net-at-resolver-function-level

Links:
https://apollo-angular.com/

https://dev.to/berviantoleo/series/15056

https://graphql-aspnet.github.io/docs/development/unit-testing#create-a-test-server

#### Subscription .dot net
https://graphql-dotnet.github.io/docs/getting-started/subscriptions/

https://github.com/graphql-dotnet/server

#### Database
Data migration using (localdb)\ProjectsV13

PM> update-database


#### Mutataion
```
mutation
{
	createNote(message:"Test")
	{
  	id
  	message
  
	}
}

```

#### Query
Query 1
```
{
  notesFromEF
  {
    id,
    message
  }
}

```
Query 2

```
{
  notes
  {
    id,
    message
  }
}
```

