FROM quay.io/davespanton/gforth

RUN useradd --create-home -s /bin/bash forth
WORKDIR /home/forth
USER forth

RUN mkdir /home/forth/mars

ADD ./lib /home/forth/mars/lib
ADD ./src /home/forth/mars/src
ADD ./test /home/forth/mars/test
ADD ./values.txt /home/forth/mars/values.txt

WORKDIR /home/forth/mars

CMD ["gforth", "src/mars.fs"]
