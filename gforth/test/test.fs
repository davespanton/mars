#! /usr/local/bin/gforth

require ../lib/test-more.fs
require ../src/mars.fs
require test-helper.fs

12 plan

diag" Testing mars...\n"

{sp

s\" \n1 1 E\n3 3 N LOST\n2 3 S" s" values.txt" str-test
=ok" provided example" ?sp

s\" \n1 3 N LOST\n3 1 E LOST\n1 0 S LOST\n0 1 W LOST" s" test/inputs/lost.txt" str-test
=ok" gets lost" ?sp

s\" \n1 3 N LOST\n1 3 N" s" test/inputs/scent.txt" str-test
=ok" saved by scent" ?sp

s" test/inputs/xbound.txt" err-test =ok" throws X out of bounds exception" 2drop ?sp

s" test/inputs/ybound.txt" err-test =ok" throws Y out of bounds exception" 2drop ?sp

s" test/inputs/overhundred.txt" err-test =ok" throws instruction too long exception" 2drop ?sp

}sp

bye
