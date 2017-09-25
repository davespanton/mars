require move.fs

105 Value inbuf-len
100 Value instructions-len

: print-position ( x y o ? -- )
    >r rot . swap . emit space r> true = if ." LOST" endif cr ;

: parse-instruction ( c -- )
    case
        [char] F of forward endof
        [char] L of left endof
        [char] R of right endof
    endcase ;

: read-instruction
    + c@ parse-instruction ;

: run-instructions { xt addr len -- } 
    len 0 ?do 
        addr i read-instruction dup true = if
            leave 
        else 
            i len 1 - < if 
                drop 
            endif 
        endif
    loop
    xt execute ;

: check-instruction-length ( read-len -- )
    instructions-len = if s" instruction too long" exception throw endif ;

: line>buf { fid buf -- }
    buf inbuf-len blank buf instructions-len fid read-line throw drop
    dup check-instruction-length ;

: read-body { out-xt fid buf -- }
    begin
        fid file-position drop fid file-size drop d< 
    while
        fid buf line>buf drop \ blank line
        fid buf line>buf buf swap start-position
        out-xt fid buf line>buf buf swap run-instructions
    repeat ;

: read-grid ( fid buf -- addr len )
    tuck line>buf ;

: read-instructions ( outxt addr u -- )
    r/o open-file throw { fid }
    inbuf-len chars allocate throw { buf }
    fid buf read-grid set-grid
    fid buf read-body
    buf free throw
    fid close-file throw ;

: main ( addr u -- )
    ['] print-position -rot read-instructions ;

s" MARS_INPUT" getenv dup [if] main bye [else] 2drop [endif]
