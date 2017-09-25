This is a Gforth implementation of lpil's [Mars](https://github.com/lpil/mars) challenge. For further background refer to the main README.

## Dependencies

This was written and tested against `gforth 0.7.9_20170705 amd64` (built from source). Older version may not work due to the use of some newer words.

Rather than build it from source, you can use the included Dockerfile or [image](https://quay.io/repository/davespanton/forthmars), derived from this base [image](https://quay.io/repository/davespanton/gforth). Using this requires [Docker](https://www.docker.com/get-docker).

## Build (Docker)

There's no need to build if running locally, only if building the Docker image, which is as simple as:

    docker build -t $YOUR_TAG .

## Run

To run with a local gforth:

    MARS_INPUT=values.txt gforth src/mars.fs

or with Docker:

    docker run -it --rm -e MARS_INPUT="values.txt" quay.io/davespanton/forthmars

If the `MARS_INPUT` environment variable is undefined, then it will drop into interactive mode. From here you could achieve the same output as above with:

    ' print-position s" values.txt" read-instructions

## Test

You can run the tests (which output in [TAP](https://testanything.org/) format by running:

    gforth test/test.fs

You'll get better output by using a TAP harness such as [prove](https://linux.die.net/man/1/prove):

    prove test/test.fs

Without a local gforth, you can run the tests with Docker:

    docker run -it --rm quay.io/davespanton/forthmars prove test/test.fs

## Other

This is only a bit of fun, it doesn't do a huge amount of input validation or bounds checking. ¯＼\_(ツ)\_/¯

Uses AndyA's excellent [gforth-tap](https://github.com/AndyA/gforth-tap) libary.
