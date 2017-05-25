# ScupTel
[![TravisCI](https://travis-ci.org/hdrezei/scuptel.svg?branch=master)](https://travis-ci.org/hdrezei/scuptel)
[![CircleCI](https://circleci.com/gh/hdrezei/scuptel.svg?style=shield)](https://circleci.com/gh/hdrezei/scuptel)
[![Docker](https://img.shields.io/docker/automated/jrottenberg/ffmpeg.svg)](https://www.docker.com/community-edition)

### Start Application
* Install Docker

* Share Hard Drives and 4GB of memory (Windows and iOS)
  - [Docker for Mac](https://docs.docker.com/docker-for-mac/)
  - [Docker for Windows](https://docs.docker.com/docker-for-windows/)
  
* Run **docker-compose up -d**
* Run **docker logs scuptel.api**

### Endpoints
* localhost:5000/Home (GET)

* localhost:5000/Produtos (GET)

* localhost:5000/Produtos/{id} (GET)

* localhost:5000/Produtos (POST)
  - *BodyParameters*
      { "Id": guid, "Nome": string, "MinutosFranquia": int }
      
* localhost:5000/Chamadas/SimulacaoProdutoFaleMais
  - *Parameters*
      - dddOrigem : int
      - dddDestino : int
      - tempoChamada : int
      - produtoFaleMaisId : guid
