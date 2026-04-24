## CatalogApi
### DockerCompose for CatalogApi
* Add docker support file in CatalogApi.proj
* Add docker compose support file in CatalogApi.proj
* Edit docker compose override file by:
    * Set depends_on
    * Change ports when necesary
    * Add ConnectionStrings to override the same config in appsetttings.jon in Api.Proj
* BUild and Run api proj as docker compose
* Test Apis via Postman