require scent.fs

variable xmax
variable ymax

: north ( -- c-addr u ) s" NWE" ;

: east ( -- c-addr u ) s" ENS" ;

: south ( -- c-addr u ) s" SEW" ;

: west ( -- c-addr u ) s" WSN" ;

: parse-orientation ( c -- c-addr u )
    case
        [char] N of north endof
        [char] E of east endof
        [char] S of south endof
        [char] W of west endof
    endcase ;

: change-direction { n o1 -- o2 } o1 parse-orientation n chars - + c@ ;

: left ( o1 -- o2 ? ) 2 swap change-direction false ;

: right ( o1 -- o2 ? ) 1 swap change-direction false ;

: lost { x y o -- x y o ? }
    x y o scent scent-contains if 
        x y o false 
    else
        x y o scent-push
        x y o true
    endif ;

: forward { x1 y1 o -- x2 y2 o ? } 
    o case
        [char] N of y1 ymax @ = if x1 y1 o lost else x1 y1 1+ o false endif endof
        [char] E of x1 xmax @ = if x1 y1 o lost else x1 1+ y1 o false endif endof
        [char] S of y1 0 = if x1 y1 o lost else x1 y1 1- o false endif endof
        [char] W of x1 0 = if x1 y1 o lost else x1 1- y1 o false endif endof
    endcase ;

: parse-number ( addr len -- n )
    2>r 0 s>d 2r> >number 2drop d>s ;

: check-boundary ( n -- )
    dup 50 > if s" boundary too large" exception throw endif ;

: set-xmax ( n -- )
    check-boundary xmax ! ;

: set-ymax ( n -- )
    check-boundary ymax ! ;

: set-grid ( addr len -- )
    32 $split parse-number set-ymax parse-number set-xmax ;

: start-position ( addr len -- x y o )
    32 $split 32 $split
    drop c@ >r
    parse-number >r
    parse-number r> r> ;
