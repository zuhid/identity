################################################## variables ##################################################
baseUrl="http://localhost:5215"
ouptutDir="TestResults"

################################################## functions ##################################################

get() {
  curl -i -s -X "GET" "$baseUrl/$1" >"$ouptutDir//${1//\//__}.get.json"
}

post() {
  curl -i -s -X "POST" "$baseUrl/$1" -d "$2" -H 'Content-Type: application/json' >"$ouptutDir//${1/\//__}.post.json"
}

put() {
  curl -i -s -X "PUT" "$baseUrl/$1" -d "$2" -H 'Content-Type: application/json' >"$ouptutDir//${1/\//__}.put.json"
}

delete() {
  curl -i -s -X "DELETE" "$baseUrl/$1" >"$ouptutDir/${1//\//__}.delete.json"
}

################################################## main ##################################################
clear
rm -rf TestResults
mkdir TestResults
userId=$(uuidgen)

echo $userId
# get 'User'
# post 'User' '{
#   "id": "f08b13f0-1e24-48f7-a205-029de54eedc6",
#   "updated": "2025-03-14T14:43:12.702Z",
#   "updatedBy": "string",
#   "isActive": true,
#   "email": "test@test.com",
#   "password": "P@ssw0rd",
#   "phone": "333-333-3333"
# }'

# put 'User' '{
#   "id": "f08b13f0-1e24-48f7-a205-029de54eedc6",
#   "updated": "2025-03-14T16:59:05.2120027",
#   "updatedBy": "string",
#   "isActive": true,
#   "email": "test2@test.com",
#   "password": "P@ssw0rd",
#   "phone": "222-444-4444"
# }'
# get "User?Id=f08b13f0-1e24-48f7-a205-029de54eedc6"
# delete "User/Id/f08b13f0-1e24-48f7-a205-029de54eedc6"
