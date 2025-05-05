echo "Downloading SchemaSpy v6.2.4 from https://github.com/schemaspy/schemaspy/releases/download/v6.2.4/schemaspy-6.2.4.jar"
curl -L "https://github.com/schemaspy/schemaspy/releases/download/v6.2.4/schemaspy-6.2.4.jar" --output ./schemaspy.jar

echo "Downloading Postgres JDBC driver"
curl -L "https://jdbc.postgresql.org/download/postgresql-42.5.4.jar" --output ./postgres-jdbc-driver.jar

echo "Generating Diagram"
java -jar ./schemaspy.jar -t pgsql11 -dp ./postgres-jdbc-driver.jar -db AnimalTrack -host localhost -post 5432 -u postgres -p WhatTheFooBar123 -o ./schema-export