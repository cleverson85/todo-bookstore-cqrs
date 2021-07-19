#!/bin/bash
mongo -- "$MONGO_INITDB_DATABASE" <<EOF
    var rootUser = '$MONGO_INITDB_ROOT_USERNAME';
    var rootPassword = '$MONGO_INITDB_ROOT_PASSWORD';
    var admin = db.getSiblingDB('$MONGO_INITDB_AUTHENTICATION_DATABASE');
    admin.auth(rootUser, rootPassword);
    db.createUser({user: rootUser, pwd: rootPassword, roles: ["dbOwner"]});
EOF